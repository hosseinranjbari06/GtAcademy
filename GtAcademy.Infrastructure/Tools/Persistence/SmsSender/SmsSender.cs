using ErrorOr;
using GtAcademy.Application.Tools.SmsSender;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Tools.Persistence.SmsSender
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsSenderSettings _settings;

        public SmsSender(IOptions<SmsSenderSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<ErrorOr<bool>> SendVerificationCode(string phoneNumber, string code)
        {
            try
            {
                SmsIr smsIr = new SmsIr(_settings.ApiKey);

                VerifySendParameter[] verifySendParameters = { new VerifySendParameter("CODE", code) };

                var response = await smsIr.VerifySendAsync(phoneNumber, _settings.TemplateId, verifySendParameters);
                
                if (response.Status.ToString() == "200") return true;

                return false;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.GetType().Name, ex.Message);
            }
        }
    }
}
