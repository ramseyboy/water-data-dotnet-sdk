#!/usr/bin/env  pwsh

dotnet test --logger trx --collect:"XPlat Code Coverage" --settings coverlet.runsettings
