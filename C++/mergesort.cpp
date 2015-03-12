/*
 * mergesort.cpp
 *
 *  Created on: Mar 6, 2014
 *      Author: Raghuveer Sagar
 */

#include<iostream>
#include<vector>
#include<sstream>
#include<iterator>
#include<climits>


using namespace std;

int number_of_comparisions=0;
void printvector_1(vector<int> &v) {
	copy(v.begin(), v.end(), ostream_iterator<int>(cout, " "));
	cout << endl;

}


void merge(vector<int> &arr,int p,int q,int r)
{
	vector<int> l1(arr.begin() + p,arr.begin() + (q+1));
	vector<int> l2(arr.begin() + (q+1),arr.begin() + r);
	l1.push_back(INT_MAX);
	l2.push_back(INT_MAX);
	int i=0;
	    int j=0;
	    for(int k=p;k<r;k++){
	        if(l1[i]<=l2[j])
		{
	            arr[k]=l1[i];
	            i++;
	        }
	        else
	        {
	            arr[k]=l2[j];
	            j++;
	        }
	        number_of_comparisions++;
	    }


}

void mergesort(vector<int> &arr,int p,int r)
{


	if (p<r){
	     int q=(p+r)/2;
	        mergesort(arr,p,q);
	        mergesort(arr,q+1,r);
	        merge(arr,p,q,r);
	}

}



int main() {

	string inputLine = string();
	getline(cin, inputLine);
	istringstream stream(inputLine);
	vector<int> values;
	clock_t t1,t2;
	int n;
	while (stream >> n) {

		values.push_back(n);
	}
	//printvector_1(values);
	 t1 = clock();

	mergesort(values,0,values.size()-1);
	t2 = clock();
	float diff ((float)t2-(float)t1);
	float seconds = diff/CLOCKS_PER_SEC;
	cout<<"Total time taken in milli-seconds "<<1000*seconds<<endl;
	cout<<"Number of comparisions "<<number_of_comparisions<<endl;
	//printvector_1(values);

}



