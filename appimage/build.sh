if [ ! -d build ]; then
	mkdir build
fi
cd build
if [ -d AppDir ]; then 
	rm -rf AppDir
fi
mkdir AppDir

if [ ! -f ../../Build/Build.x86_64 ]; then
	echo "Couldn't find the build. Please compile the game via unity first"
	exit
fi

if [ ! -f ./linuxdeploy-x86_64.AppImage ]; then
	echo "Downloading linuxdeploy appimage" 
	wget 'https://github.com/linuxdeploy/linuxdeploy/releases/download/continuous/linuxdeploy-x86_64.AppImage'
fi
chmod +x ./linuxdeploy-x86_64.AppImage

cp -r ../../Build/* ./AppDir
./linuxdeploy-x86_64.AppImage --appdir ./AppDir --custom-apprun=../run.sh -i ./AppDir/Build_Data/Resources/UnityPlayer.png --desktop-file ../PrzyslowiowyPlatformer.desktop --output appimage
cp ./Przysłowiowy_Platformer-*-x86_64.AppImage ../
