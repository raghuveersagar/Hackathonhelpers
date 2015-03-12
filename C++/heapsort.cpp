#include<iostream>
#include<vector>
#include<sstream>
#include<algorithm>
#include<iterator>
using namespace std;

struct heap {

	vector<int> arr;
	int heapsize;
	int length;
	heap(vector<int> v) :
			arr(v), heapsize(0), length(v.size()) {

	}
};

int number_of_comparisions=0;
void exchange(vector<int> &v, int a, int b) {

	int temp = v[a];
	v[a] = v[b];
	v[b] = temp;

}
void printvector(vector<int> &v) {
	copy(v.begin(), v.end(), ostream_iterator<int>(cout, " "));
	cout << endl;

}

void maxHeapify(heap &h, int i) {
	int right = 2 * i + 1;
	int left = 2 * i + 2;
	vector<int> &arr = h.arr;
	int largest = -1;
	if (left <= h.heapsize - 1 && arr[i] < arr[left]) {
		largest = left;

	} else {
		largest = i;

	}
	if (right <= h.heapsize - 1 && arr[largest] < arr[right]) {
		largest = right;

	}
	if (largest != i) {
		//cout<<"i "<<i<<" largest "<<largest<<endl;

		exchange(arr, i, largest);

		maxHeapify(h, largest);
	}
	number_of_comparisions=number_of_comparisions+3;

}

void buildMaxHeap(heap& h) {
	int length = h.length;
	h.heapsize = h.length;
	for (int i = (h.length - 1) / 2; i >= 0; i--)
		maxHeapify(h, i);

}

void heapsort(heap &h) {
	buildMaxHeap(h);
	for (int i = h.length - 1; i >= 0; i--) {
		exchange(h.arr, i, 0);
		h.heapsize = h.heapsize - 1;
		maxHeapify(h, 0);

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
	//printvector(values);

	heap h(values);
	h.heapsize = h.length;
	t1 = clock();
	heapsort(h);
	t2 = clock();
	float diff ((float)t2-(float)t1);
	float seconds = diff/CLOCKS_PER_SEC;
	cout<<"Total time taken in milli-seconds "<<1000*seconds<<endl;
	cout<<"Number of comparisions "<<number_of_comparisions<<endl;
	//printvector(h.arr);

}



