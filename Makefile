
.PHONY: clean all all_debug

all:
	mdtool build -c:Release --buildfile: QuickRoute.sln

all_debug:
	mdtool build -c:Debug --buildfile: QuickRoute.sln

clean:
	mdtool build -t:Clean
