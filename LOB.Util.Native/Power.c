#include <stdio.h>

double __declspec(dllexport) Power(double x,double y){
	double result = x;
	int k;
	for(k = 1; k < y; k++){
		result *= x;
	}
	return result;
}