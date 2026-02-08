using System;
using System.Collections.Generic;
using System.Text;

namespace MFO.Application.Authentication;

public sealed record AuthResponse(
    Guid UserId,
    string Email,
    string AccessToken,
    DateTimeOffset ExpiresAt);