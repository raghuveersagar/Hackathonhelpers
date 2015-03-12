import java.util.Arrays;

/**
 * Modulus_Large_Number --- Calculate Modulus of large numbers Non-exponent".
 * 
 * @author Raghuveeer Sagar
 */

// Number N can be any number of digits
// Modulus M has be <=17 digits

public class Modulus_Large_Number {

	final int max_digits = 18;

	public static void main(String[] args) {

		System.out
				.println(new Modulus_Large_Number()
						.calculatelargeModulus(
								"31415926535897932384626433832795028841971",
								7771377737973737L));

	}
	
	 /**
	   * Calculate Modulus of large  
	   * numbers.Non-exponent.
	   * @param N A String representing number N 
	   * @param.M a long value representing the modulus M. 
	   * @exception Any exception
	   * @return return long representing the value = N (mod M).
	   */

	long calculatelargeModulus(String N, long M) {
		char[] num_arr = N.toCharArray();
		final int initial_length = num_arr.length;
		int length_so_far = 0;
		int length_Mod_part = 0;
		int rem_str_len = initial_length;
		int aval_str_len = max_digits;
		String remaining_num_str = "";
		aval_str_len = (max_digits - length_Mod_part);
		rem_str_len = (initial_length - length_so_far);
		while (rem_str_len > aval_str_len) {

			remaining_num_str = remaining_num_str
					+ new String(Arrays.copyOfRange(num_arr, length_so_far,
							length_so_far + aval_str_len));
			// logs -- uncomment the below line for logs
			// System.out.print(remaining_num_str+"%"+M);
			remaining_num_str = String.valueOf(Long.valueOf(remaining_num_str)
					% M);
			// logs--uncomment the below line for logs
			// System.out.println("= "+remaining_num_str);
			length_so_far = length_so_far + aval_str_len;
			length_Mod_part = remaining_num_str.length();
			aval_str_len = (max_digits - length_Mod_part);
			rem_str_len = (initial_length - length_so_far);
		}

		remaining_num_str = remaining_num_str
				+ new String(Arrays.copyOfRange(num_arr, length_so_far,
						initial_length));
		remaining_num_str = String.valueOf(Long.valueOf(remaining_num_str) % M);

		return Long.valueOf(remaining_num_str);
	}

}
