UnbindSourceCodeFromTFS
=======================

Unbind Visual Studio source code from TFS

The program follow the next steps:

1. Create a backup
2. Remove the read only attribute of all folders
3. Search for ```*.scc``` , ```*.vssscc```, ```*.vspscc``` to delete them
4. Delete section ```GlobalSection(TeamFoundationVersionControl)``` from solution files
5. Delete ```SccProjectName```, ```SccLocalPath```, ```SccAuxPath```, ```SccProvider``` from project files

:octocat:
