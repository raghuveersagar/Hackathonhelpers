#include<vector>
#include<string>
#include<memory>
#include<fstream>
#include<iostream>
#include<map>
#include<algorithm>
#include <stdlib.h> 
using namespace std;


/*

Data type representing the heap
*/
template<class T>
struct heap
{

	vector<T> arr;
	int heapsize;
	int length;
	heap(vector<T> v) :
		arr(v), heapsize(0), length(v.size()) {

	}


};


/*
Data type representing each vertex in the graph
*/
struct vertex
{

	vertex(int d, vertex * p, string v, int pos) :distance(d), parent(p), value(v), heap_position(pos){

	}
	int distance;
	vertex* parent;
	string value;
	int heap_position;


	bool operator>(const vertex& v1)
	{
		return this->distance > v1.distance;
	}

};

ostream& operator<<(ostream& os, heap<vertex*>& h)
{

	vector<vertex*>& arr = h.arr;
	for (int i = 0; i < h.heapsize; i++)
		os << arr[i]->value << "(" << arr[i]->distance << "," << arr[i]->heap_position << ") ";

	return os;
}




class priority_q
{

public:
	heap<vertex*> h;
	priority_q(vector<vertex*> v) :h(v){
		h.heapsize = h.length;
		buildMinHeap();
	}

	vertex* pop()
	{
		return extract_min();
	}


	vertex* top()
	{
		return h.arr[0];
	}

	bool empty()
	{
		return h.heapsize == 0;
	}

	void buildMinHeap() {
		int length = h.length;
		h.heapsize = h.length;
		for (int i = (h.length - 1) / 2; i >= 0; i--)
			minHeapify(i);

	}

	void minHeapify(int i) {


		int right = 2 * i + 1;
		int left = 2 * i + 2;
		vector<vertex*> &arr = h.arr;
		int smallest = -1;
		if (left <= h.heapsize - 1 && (arr[i])->distance > (arr[left])->distance) {
			smallest = left;

		}
		else {
			smallest = i;

		}
		if (right <= h.heapsize - 1 && (arr[smallest])->distance > (arr[right])->distance) {
			smallest = right;

		}
		if (smallest != i) {
			exchange(arr, i, smallest);
			minHeapify(smallest);
		}
	}







	vertex* extract_min()
	{
		vertex* temp = h.arr[0];
		vector<vertex*>& temp_arr = h.arr;
		temp_arr[0] = temp_arr[h.heapsize - 1];
		temp_arr[0]->heap_position = 0;
		h.heapsize = h.heapsize - 1;
		minHeapify(0);
		return temp;


	}

	void print_heap()
	{
		cout << h << endl;
	}


	void exchange(vector<vertex*> &v, int a, int b) {




		vertex* temp = v[a];

		v[a] = v[b];
		v[b] = temp;
		int temp_pos = v[a]->heap_position;
		v[a]->heap_position = v[b]->heap_position;
		v[b]->heap_position = temp_pos;



	}


	void update_key(vertex* v)
	{


		int pos = v->heap_position;

		vector<vertex*> &temp = (h.arr);
		while (pos > 0 && temp[pos]->distance < temp[(pos - 1) / 2]->distance)
		{

			exchange(temp, pos, ((pos - 1) / 2));

			pos = ((pos - 1) / 2);

		}

	}



};