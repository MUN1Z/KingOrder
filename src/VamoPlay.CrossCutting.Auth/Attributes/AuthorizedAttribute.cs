﻿using VamoPlay.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
namespace VamoPlay.CrossCutting.Auth.Attributes
{
    public sealed class AuthorizedAttribute : AuthorizeAttribute
    {
        public const string ClaimsGroup = "Claims";

        private UserClaim[] _claims;

        private bool _isDefault = true;

        public AuthorizedAttribute()
        {
            _claims = Array.Empty<UserClaim>();
        }

        public AuthorizedAttribute(params UserClaim[] userClaims)
        {
            _claims = Array.Empty<UserClaim>();
            UserClaims = userClaims;
        }

        public UserClaim[] UserClaims
        {
            get => _claims;
            set
            {
                _claims = value;
                BuildPolicy(_claims.Select(c => c.ToString()).ToArray(), ClaimsGroup);
            }
        }

        private void BuildPolicy(string[] target, string group)
        {
            if (_isDefault)
            {
                Policy = string.Empty;
                _isDefault = false;
            }

            Policy += $"{group}${string.Join("|", target)};";
        }
    }
}