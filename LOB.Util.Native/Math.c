#include <stdio.h>
#include <stdlib.h>
#include <time.h>

double __declspec(dllexport) Power(double *x, double y);
double __declspec(dllexport) Random();

double __declspec(dllexport) Power(double *x,double y){
	double result = *x;
	int k;
	for(k = 1; k < y; k++){
		result *= *x;
	}
	return result;
}

double __declspec(dllexport) Random(){
	srand(time(NULL));
	return rand();
}
