using System;
using System.Collections.Generic;
using System.Text;

namespace OAUS.Core
{
    public interface IOausService
    {
        int GetLatestVersion();

        double GetLatestVersion(string projectName);

        Dictionary<string, double> getRemoteConfig(string projectName);
    }
}
