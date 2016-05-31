git pull origin master

nuget restore
xbuild /p:Configuration=Release

nuget pack SparkPost/SparkPost.nuspec -Prop Configuration=Release

PACKAGE=$(ls *.nupkg)

# This will unzip the nupkg, then zip it back up.
# This is done to some issue with the nuget command
# line app creating nupkg files that cannot be
# used by Visual Studio. 
# https://github.com/NuGet/Home/issues/2833
mv $PACKAGE file.zip
mkdir stuff
cd stuff
unzip ../file.zip
rm ../file.zip
zip -r ../file.zip *
cd ..
mv file.zip $PACKAGE

nuget push $PACKAGE $APIKEY -s nuget.org
