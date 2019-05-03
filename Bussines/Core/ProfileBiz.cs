using System;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class ProfileBiz : BizBase<ProfileKeyValue>
    {
        public ProfileBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public void SetUserProfile(int userId, IEnumerable<ProfileKeyValue> profileKeyValues)
        {
            var keys = profileKeyValues.Select(keyValue => keyValue.Type).Distinct();
            var currentKeyValues = Read(textProfile => keys.Contains(textProfile.Type) && textProfile.UserId == userId).ToList();

            foreach (var keyValue in profileKeyValues) {
                var oldKeyValue = currentKeyValues.SingleOrDefault(kv => kv.Type == keyValue.Type);

                if (oldKeyValue != null) {
                    // Update or Delete an old KeyValue
                    if (string.IsNullOrWhiteSpace(keyValue.Value))
                        Remove(oldKeyValue);
                    else
                        oldKeyValue.Value = keyValue.Value.Trim();
                }
                else {
                    // Add New KeyValue
                    if (string.IsNullOrWhiteSpace(keyValue.Value))
                        continue;
                    Add(new ProfileKeyValue() {
                        UserId = userId,
                        Type = keyValue.Type,
                        Value = keyValue.Value.Trim(),
                    });
                }
            }
        }

        public void SetUserProfile(int userId, ProfileKeyValueType key, string value)
        {
            SetUserProfile(userId, new ProfileKeyValue[] {
                new ProfileKeyValue() {
                    UserId = userId,
                    Type = key,
                    Value = value
                }
            });
        }

        public string ReadUserProfileValue(int userId, ProfileKeyValueType key)
        {
            return ReadSingle(kv => kv.UserId == userId && kv.Type == key).Value;
        }

        public bool TryReadUserProfileValue(int userId, ProfileKeyValueType type, out string value)
        {
            var keyValue = ReadSingleOrDefault(kv => kv.UserId == userId && kv.Type == type);
            value = keyValue?.Value;
            return value != null;
        }

        public IEnumerable<ProfileKeyValue> ReadUserProfileValues(int userId, params ProfileKeyValueType[] keys)
        {
            return Read(kv => kv.UserId == userId && keys.Contains(kv.Type)).ToList();
        }
    }
}