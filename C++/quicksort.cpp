/*
 * quicksort.cpp
 *
 *  Created on: Mar 4, 2014
 *      Author: Raghuveer Sagar
 */
#include<iostream>
#include<string>
#include<sstream>
#include <iterator>
#include<vector>
#include<algorithm>
#include<time.h>
using namespace std;

int number_of_partitions = 0;
int number_of_comparisions = 0;

void swap(vector<int> &v, int i, int j) {
	int temp = v[i];
	v[i] = v[j];
	v[j] = temp;

}

void printvector_2(vector<int> &v) {
	copy(v.begin(), v.end(), ostream_iterator<int>(cout, " "));
	cout << endl;

}

int partition(vector<int> &v, int p, int r) {

	number_of_partitions++;

	int x = v[r];
	int i = p - 1;
	for (int j = p; j < r; j++) {
		if (v[j] <= x) {
			i = i + 1;
			swap(v, i, j);
		}
		number_of_comparisions++;
	}
	swap(v, i + 1, r);
	return i + 1;
}

void quicksort(vector<int> &v, int p, int r) {

//cout<<"Inside quicksort "<<endl;
	//cout<<"p "<<p<<" r "<<r<<endl;
	int q;
	if (p < r) {
		q = partition(v, p, r);

		quicksort(v, p, q - 1);
		quicksort(v, q + 1, r);
	}

}


int main() {

	string inputLine = string();
	getline(cin, inputLine);
	istringstream stream(inputLine);
	vector<int> values;
	clock_t t1, t2;
	int n;
	while (stream >> n) {
		//cout<<n<<endl;
		values.push_back(n);
	}
	//printvector(values);
	//bsort(values,0,values.size()-1);
	t1 = clock();
	quicksort(values, 0, values.size() - 1);
	t2 = clock();
	float diff((float) t2 - (float) t1);
	float seconds = diff / CLOCKS_PER_SEC;
	cout << "Total time taken in milli-seconds " << 1000 * seconds << endl;
	cout << "Number of comparisions " << number_of_comparisions << endl;
	cout << "Number of partitions " << number_of_partitions << endl;
	//printvector_2(values);
}


