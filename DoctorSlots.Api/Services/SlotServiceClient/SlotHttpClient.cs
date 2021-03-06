﻿using DoctorSlots.Api.Models;
using DoctorSlots.Api.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace DoctorSlots.Api.Services.SlotServiceClient
{
    public class SlotHttpClient : AuthHttpClient
    {
        private readonly string _baseAddress;
        private readonly string _username;
        private readonly string _password;

        private readonly IEncoder _encoder;

        public SlotHttpClient(
            IOptions<SlotServiceConfiguration> serviceConfiguration, 
            IEncoder encoder, 
            IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory)
        {

            _baseAddress = serviceConfiguration.Value.BaseAddress;
            _username = serviceConfiguration.Value.Username;
            _password = serviceConfiguration.Value.Password;

            _encoder = encoder;
        }

        public override string BaseAddress
        {
            get
            {
                return _baseAddress;
            }
        }

        protected override AuthenticationHeaderValue GetAuthorizationHeader()
        {
            // Basic Authentication. Format: username:password encoded in Base64
            string header = string.Concat(_username, ":", _password);
            return new AuthenticationHeaderValue("Basic", _encoder.EncodeBase64(header));
        }
    }
}
