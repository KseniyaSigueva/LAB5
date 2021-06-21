#include <cmath>
#include <string>

using namespace std;

extern "C" {
	__declspec(dllexport)
	double time(double a, double b)
	{
		return b - a;
	}

	__declspec(dllexport)
		double amplitude(const char* func) {
		double amplitude = 0;
		string tmp = func;
		if (tmp.substr(0, tmp.find("sin")) == "")
			amplitude = 1;
		else amplitude = stod(tmp.substr(0, tmp.find("sin")));

		return amplitude;
	}
}

