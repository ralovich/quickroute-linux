
.PHONY: clean all all_debug

all:
#	mdtool build -c:Release --buildfile: QuickRoute.sln
	xbuild /p:Configuration=Release QuickRoute.sln

all_debug:
#	mdtool build -c:Debug --buildfile: QuickRoute.sln
	xbuild /p:Configuration=Debug QuickRoute.sln

clean:
#	mdtool build -t:Clean
	xbuild /t:Clean /p:Configuration=Debug QuickRoute.sln
	xbuild /t:Clean /p:Configuration=Release QuickRoute.sln
	rm -rf QuickRoute.UI/bin
	rm -rf QuickRoute.UI/obj

#  dh_auto_install
#    make -j1 install DESTDIR=/home/tade/dev/clean/2.7/1/debian/quickroute-gps

install:
	@echo "install being called"
	@echo $:
	@echo $@
	mkdir --parents ${DESTDIR}/usr/bin ${DESTDIR}/usr/lib/quickroute-gps
	cp scripts/quickroute-gps ${DESTDIR}/usr/bin
	cp QuickRoute.UI/bin/Release/QuickRoute.exe ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.exe.config ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/ExifWorks.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/Interop.EARTHLib.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/Ionic.Zip.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/log4net.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/PowerCollections.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.BusinessEntities.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.Common.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.Controls.dll ${DESTDIR}/usr/lib/quickroute-gps
	#cp QuickRoute.UI/bin/Release/QuickRoute.GPSDeviceReaders.GarminUSBReader.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.GPSDeviceReaders.GlobalSatGH615MReader.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.GPSDeviceReaders.JJConnectRegistratorSEReader.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.GPSDeviceReaders.SerialPortDeviceReader.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.Publishers.DOMAPublisher.dll ${DESTDIR}/usr/lib/quickroute-gps
	cp QuickRoute.UI/bin/Release/QuickRoute.Resources.dll ${DESTDIR}/usr/lib/quickroute-gps
	mkdir --parents ${DESTDIR}/usr/share/doc/quickroute-gps
	cp debian/copyright ${DESTDIR}/usr/share/doc/quickroute-gps
	mkdir --parents ${DESTDIR}/usr/share/pixmaps
	cp Graphics/QuickRoute_32x32.xpm ${DESTDIR}/usr/share/pixmaps
	ln -s -r ${DESTDIR}/usr/share/pixmaps/QuickRoute_32x32.xpm ${DESTDIR}/usr/share/pixmaps/quickroute-gps.xpm
	mkdir --parents ${DESTDIR}/usr/share/applications
	cp debian/quickroute-gps.desktop ${DESTDIR}/usr/share/applications
