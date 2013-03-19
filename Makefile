
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

install:
	@echo "install being called"
	@echo $:
	@echo $@

