msbuild SqlProject\SqlProject.sln /t:Rebuild
msbuild /t:Publish /p:SqlPublishProfilePath="SqlProject.publish.xml" SqlProject\SqlProject.sqlproj
powershell -noexit