using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Domain.Entities;
using Ss.CmbXft.Domain.Repositories;
using Ss.CmbXft.Sdk.Models.Events;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 薪福通事件处理服务实现
/// </summary>
public class XftEventService : IXftEventService
{
    private readonly IRepository<XftEventLog, Guid> _eventLogRepository;
    private readonly IRepository<StaffInfo, Guid> _staffInfoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<XftEventService> _logger;

    public XftEventService(
        IRepository<XftEventLog, Guid> eventLogRepository,
        IRepository<StaffInfo, Guid> staffInfoRepository,
        IUnitOfWork unitOfWork,
        ILogger<XftEventService> logger)
    {
        _eventLogRepository = eventLogRepository;
        _staffInfoRepository = staffInfoRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> ProcessEventAsync(XftEventPushRequest request)
    {
        _logger.LogInformation("开始处理薪福通事件 - EventId: {EventId}, BusinessKey: {BusinessKey}, EventCd: {EventCd}",
            request.EventId, request.BusinessKey, request.EventCd);

        XftEventLog eventLog = null!;

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            eventLog = new XftEventLog
            {
                EventId = request.EventId,
                BusinessKey = request.BusinessKey,
                EventCd = request.EventCd,
                PrjCod = request.PrjCod,
                EventTime = request.EventTime,
                AppId = request.AppId,
                EventRcdInf = request.EventRcdInf,
                Signature = request.Signature,
                ProcessStatus = 0,
                RetryCount = 0
            };

            await _eventLogRepository.AddAsync(eventLog);
            await _unitOfWork.SaveChangesAsync();

            var processResult = await ProcessEventByTypeAsync(request);

            eventLog.ProcessStatus = processResult ? 1 : 2;
            eventLog.ProcessMessage = processResult ? "处理成功" : "处理失败";
            await _eventLogRepository.UpdateAsync(eventLog);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("薪福通事件处理完成 - EventId: {EventId}, Success: {Success}",
                request.EventId, processResult);

            return processResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理薪福通事件时发生异常 - EventId: {EventId}", request.EventId);

            try
            {
                await _unitOfWork.RollbackTransactionAsync();

                if (eventLog != null)
                {
                    eventLog.ProcessStatus = 2;
                    eventLog.ProcessMessage = $"异常: {ex.Message}";
                    eventLog.RetryCount++;
                    _eventLogRepository.Update(eventLog);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception rollbackEx)
            {
                _logger.LogError(rollbackEx, "回滚事务时发生异常");
            }

            return false;
        }
    }

    private async Task<bool> ProcessEventByTypeAsync(XftEventPushRequest request)
    {
        switch (request.EventId)
        {
            case "XFTSTFADD":
                return await ProcessStaffAddOrUpdateEventAsync(request.EventRcdInf, isAdd: true);
            case "XFTSTFUPT":
                return await ProcessStaffAddOrUpdateEventAsync(request.EventRcdInf, isAdd: false);
            case "XFTSTFDEL":
                return await ProcessStaffDeleteEventAsync(request.EventRcdInf);
            case "XFTQUTTRADECHG":
                return await ProcessStaffQuitEventAsync(request.EventRcdInf);
            case "XFT00000":
                _logger.LogInformation("收到连接测试事件 (XFT00000)");
                return true;
            default:
                _logger.LogWarning("未处理的事件类型: {EventId}", request.EventId);
                return true;
        }
    }

    private async Task<bool> ProcessStaffAddOrUpdateEventAsync(string eventRcdInf, bool isAdd)
    {
        try
        {
            var staffEvent = JsonSerializer.Deserialize<StaffEvent>(eventRcdInf);
            if (staffEvent == null || string.IsNullOrEmpty(staffEvent.StfSeq))
            {
                _logger.LogError("员工事件数据无效");
                return false;
            }

            _logger.LogInformation("处理员工{Action}事件 - StfSeq: {StfSeq}, StfNam: {StfNam}",
                isAdd ? "新增" : "修改", staffEvent.StfSeq, staffEvent.StfNam);

            var existingStaff = await _staffInfoRepository.FirstOrDefaultAsync(s => s.StfSeq == staffEvent.StfSeq);

            if (existingStaff == null)
            {
                var newStaff = MapToStaffInfo(staffEvent);
                await _staffInfoRepository.AddAsync(newStaff);
                _logger.LogInformation("创建新员工信息 - StfSeq: {StfSeq}", staffEvent.StfSeq);
            }
            else
            {
                MapToStaffInfo(staffEvent, existingStaff);
                _staffInfoRepository.Update(existingStaff);
                _logger.LogInformation("更新员工信息 - StfSeq: {StfSeq}", staffEvent.StfSeq);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理员工{Action}事件时发生异常", isAdd ? "新增" : "修改");
            return false;
        }
    }

    private async Task<bool> ProcessStaffDeleteEventAsync(string eventRcdInf)
    {
        try
        {
            var deleteEvent = JsonSerializer.Deserialize<StaffDeleteEvent>(eventRcdInf);
            if (deleteEvent == null || string.IsNullOrEmpty(deleteEvent.StfSeq))
            {
                _logger.LogError("删除员工事件数据无效");
                return false;
            }

            _logger.LogInformation("处理删除员工事件 - StfSeq: {StfSeq}", deleteEvent.StfSeq);

            var staff = await _staffInfoRepository.FirstOrDefaultAsync(s => s.StfSeq == deleteEvent.StfSeq);
            if (staff != null)
            {
                staff.IsDeleted = true;
                staff.UpdateTime = DateTime.UtcNow;
                await _staffInfoRepository.UpdateAsync(staff);
                _logger.LogInformation("标记员工为已删除 - StfSeq: {StfSeq}", deleteEvent.StfSeq);
            }
            else
            {
                _logger.LogWarning("未找到要删除的员工 - StfSeq: {StfSeq}", deleteEvent.StfSeq);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理删除员工事件时发生异常");
            return false;
        }
    }

    private async Task<bool> ProcessStaffQuitEventAsync(string eventRcdInf)
    {
        try
        {
            var quitEvent = JsonSerializer.Deserialize<StaffQuitEvent>(eventRcdInf);
            if (quitEvent == null || string.IsNullOrEmpty(quitEvent.StaffSeq))
            {
                _logger.LogError("离职事件数据无效");
                return false;
            }

            _logger.LogInformation("处理离职事件 - StaffSeq: {StaffSeq}, ActionType: {ActionType}, ApproveStatus: {ApproveStatus}, TrsSeq: {TrsSeq}",
                quitEvent.StaffSeq, quitEvent.ActionType, quitEvent.ApproveStatus, quitEvent.TrsSeq);

            var staff = await _staffInfoRepository.FirstOrDefaultAsync(s => s.StfSeq == quitEvent.StaffSeq);
            if (staff != null)
            {
                staff.QuitTrsSeq = quitEvent.TrsSeq;
                staff.QuitActionType = quitEvent.ActionType;
                staff.QuitApproveStatus = quitEvent.ApproveStatus;
                staff.QuitUpdateTime = DateTime.UtcNow;
                staff.UpdateTime = DateTime.UtcNow;

                await _staffInfoRepository.UpdateAsync(staff);
                _logger.LogInformation("更新员工离职信息 - StfSeq: {StfSeq}, ActionType: {ActionType}",
                    quitEvent.StaffSeq, quitEvent.ActionType);
            }
            else
            {
                _logger.LogWarning("未找到要更新离职信息的员工 - StfSeq: {StfSeq}", quitEvent.StaffSeq);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理离职事件时发生异常");
            return false;
        }
    }

    private StaffInfo MapToStaffInfo(StaffEvent staffEvent, StaffInfo? target = null)
    {
        var staff = target ?? new StaffInfo();

        staff.StfSeq = staffEvent.StfSeq ?? string.Empty;
        staff.StfNbr = staffEvent.StfNbr;
        staff.StfNam = staffEvent.StfNam;
        staff.ChnNam = staffEvent.ChnNam;
        staff.UsdNam = staffEvent.UsdNam;
        staff.StfSex = staffEvent.StfSex;
        staff.Gender = staffEvent.Gender;
        staff.BirDat = TryParseDate(staffEvent.BirDat);
        staff.CerTyp = staffEvent.CerTyp;
        staff.CertType = staffEvent.CertType;
        staff.CerNbr = staffEvent.CerNbr;
        staff.CerVal = TryParseDate(staffEvent.CerVal);
        staff.CerIsL = staffEvent.CerIsL;
        staff.CerLoc = staffEvent.CerLoc;
        staff.OcrTyp = staffEvent.OcrTyp;
        staff.OthCerType = staffEvent.OthCerType;
        staff.OcrNbr = staffEvent.OcrNbr;
        staff.Nation = staffEvent.Nation;
        staff.NatnLt = staffEvent.NatnLt;
        staff.BirLad = staffEvent.BirLad;
        staff.HomPrv = staffEvent.HomPrv;
        staff.HomCty = staffEvent.HomCty;
        staff.HomAre = staffEvent.HomAre;
        staff.HomDtl = staffEvent.HomDtl;
        staff.HomTyp = staffEvent.HomTyp;
        staff.UslPrv = staffEvent.UslPrv;
        staff.UslCty = staffEvent.UslCty;
        staff.UslDst = staffEvent.UslDst;
        staff.Addres = staffEvent.Addres;
        staff.LnkPrv = staffEvent.LnkPrv;
        staff.LnkCty = staffEvent.LnkCty;
        staff.LnkDst = staffEvent.LnkDst;
        staff.LnkAdr = staffEvent.LnkAdr;
        staff.MobNbr = staffEvent.MobNbr;
        staff.TelNbr = staffEvent.TelNbr;
        staff.SelEml = staffEvent.SelEml;
        staff.WrkEml = staffEvent.WrkEml;
        staff.MarSts = staffEvent.MarSts;
        staff.AfeSts = staffEvent.AfeSts;
        staff.PltSts = staffEvent.PltSts;
        staff.MtySts = staffEvent.MtySts;
        staff.MtyNbr = staffEvent.MtyNbr;
        staff.DibSts = staffEvent.DibSts;
        staff.DibNbr = staffEvent.DibNbr;
        staff.HigSch = staffEvent.HigSch;
        staff.EntDat = TryParseDate(staffEvent.EntDat);
        staff.BgnDat = TryParseDate(staffEvent.BgnDat);
        staff.CcsDat = TryParseDate(staffEvent.CcsDat);
        staff.CorpLd = TryParseDate(staffEvent.CorpLd);
        staff.CorDat = TryParseDate(staffEvent.CorDat);
        staff.CorpLp = staffEvent.CorpLp;
        staff.PbtPrd = staffEvent.PbtPrd;
        staff.CorFlg = staffEvent.CorFlg;
        staff.CorMak = staffEvent.CorMak;
        staff.CorMsg = staffEvent.CorMsg;
        staff.CorRep = staffEvent.CorRep;
        staff.StfTyp = staffEvent.StfTyp;
        staff.StfSts = staffEvent.StfSts;
        staff.OrgSeq = staffEvent.OrgSeq;
        staff.OrgNam = staffEvent.OrgNam;
        staff.EmpCrp = staffEvent.EmpCrp;
        staff.PosUid = staffEvent.PosUid;
        staff.PosSeq = staffEvent.PosSeq;
        staff.PosNam = staffEvent.PosNam;
        staff.JobUid = staffEvent.JobUid;
        staff.JobSeq = staffEvent.JobSeq;
        staff.JobNam = staffEvent.JobNam;
        staff.JobRnk = staffEvent.JobRnk;
        staff.WkpSeq = staffEvent.WkpSeq;
        staff.RepSup = staffEvent.RepSup;
        staff.GrpSeq = staffEvent.GrpSeq;
        staff.BnkTyp = staffEvent.BnkTyp;
        staff.BnkPrv = staffEvent.BnkPrv;
        staff.BnkCty = staffEvent.BnkCty;
        staff.SalCar = staffEvent.SalCar;
        staff.DigWet = staffEvent.DigWet;
        staff.SecAct = staffEvent.SecAct;
        staff.FunAct = staffEvent.FunAct;
        staff.CstRat = TryParseDouble(staffEvent.CstRat);
        staff.WrkDdt = staffEvent.WrkDdt;
        staff.CcsDdt = staffEvent.CcsDdt;
        staff.Remark = staffEvent.Remark;
        staff.PrjCod = staffEvent.PrjCod;
        staff.OprAgn = staffEvent.OprAgn;
        staff.XftUpdateTime = TryParseDateTime(staffEvent.Update);

        if (target == null)
        {
            staff.IsDeleted = false;
        }

        return staff;
    }

    private DateTime? TryParseDate(string? dateStr)
    {
        if (string.IsNullOrEmpty(dateStr))
            return null;

        if (DateTime.TryParse(dateStr, out var date))
            return date.Date;

        return null;
    }

    private DateTime? TryParseDateTime(string? dateTimeStr)
    {
        if (string.IsNullOrEmpty(dateTimeStr))
            return null;

        if (DateTime.TryParse(dateTimeStr, out var dateTime))
            return dateTime;

        return null;
    }

    private double? TryParseDouble(string? doubleStr)
    {
        if (string.IsNullOrEmpty(doubleStr))
            return null;

        if (double.TryParse(doubleStr, out var result))
            return result;

        return null;
    }
}
