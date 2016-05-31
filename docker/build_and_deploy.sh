git pull origin master

nuget restore
xbuild /p:Configuration=Release

nuget pack SparkPost/SparkPost.nuspec -Prop Configuration=Release

PACKAGE=$(ls *.nupkg)

mv $PACKAGE file.zip
mkdir stuff
cd stuff
unzip ../file.zip
rm ../file.zip
zip -r ../file.zip *
cd ..
mv file.zip $PACKAGE

nuget push $PACKAGE $APIKEY -s nuget.org
