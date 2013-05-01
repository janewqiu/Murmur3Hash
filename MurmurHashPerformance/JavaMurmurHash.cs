using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MurmurHashPerformance
{
   
    
public   class JavaMurmurHash {

	/** Generates 32 bit hash from byte array of the given length and
	 * seed.
	 * 
	 * @param data byte array to hash
	 * @param length length of the array to hash
	 * @param seed initial seed value
	 * @return 32 bit hash of the given array
	 */
    public static int hash32(byte[] data, int length, uint seed)
    {
		// 'm' and 'r' are mixing constants generated offline.
		// They're not really 'magic', they just happen to work well.
		 int m = 0x5bd1e995;
		 int r = 24;
		// Initialize the hash to a random value
		int h =  seed^length;
		int length4 = length/4;

		for (int i=0; i<length4; i++) {
			 int i4 = i*4;
			int k = (data[i4+0]&0xff) +((data[i4+1]&0xff)<<8)
					+((data[i4+2]&0xff)<<16) +((data[i4+3]&0xff)<<24);
			k *= m;
			k ^= k >> r;
			k *= m;
			h *= m;
			h ^= k;
		}
		
		// Handle the last few bytes of the input array
		switch (length%4) {
            case 3: h ^= (data[(length & ~3) + 2] & 0xff) << 16; break;
            case 2: h ^= (data[(length & ~3) + 1] & 0xff) << 8; break;
            case 1: h ^= (data[length & ~3] & 0xff); break;
            default:
				h *= m;break;
		}

		h ^= h >> 13;
		h *= m;
		h ^= h >> 15;

		return h;
	}
	

	/** Generates 32 bit hash from byte array with default seed value.
	 * 
	 * @param data byte array to hash
	 * @param length length of the array to hash
	 * @return 32 bit hash of the given array
	 */
	public static int hash32(  byte[] data, int length) {
		return hash32( data, length, (int)0x9747b28c); 
	}


	/** Generates 32 bit hash from a string.
	 * 
	 * @param text string to hash
	 * @return 32 bit hash of the given string
	 */
	public static int hash32(  String text) {
        byte[] bytes = GetBytes(text);
		return hash32( bytes, bytes.Length);
	}


	/** Generates 32 bit hash from a substring.
	 * 
	 * @param text string to hash
	 * @param from starting index
	 * @param length length of the substring to hash
	 * @return 32 bit hash of the given string
	 */
	public static int hash32(  String text, int from, int length) {
		return hash32( text.Substring( from, from+length));
	}                
	/** Generates 64 bit hash from byte array of the given length and seed.
	 * 
	 * @param data byte array to hash
	 * @param length length of the array to hash
	 * @param seed initial seed value
	 * @return 64 bit hash of the given array
	 */
	public static long hash64(  byte[] data, int length, int seed) {
        long m = long.Parse("0xc6a4a7935bd1e995L");
		 int r = 47;

		long h = (seed&0xffffffffL)^(length*m);

		int length8 = length/8;

		for (int i=0; i<length8; i++) {
			 int i8 = i*8;
			long k =  ((long)data[i8+0]&0xff)      +(((long)data[i8+1]&0xff)<<8)
					+(((long)data[i8+2]&0xff)<<16) +(((long)data[i8+3]&0xff)<<24)
					+(((long)data[i8+4]&0xff)<<32) +(((long)data[i8+5]&0xff)<<40)
					+(((long)data[i8+6]&0xff)<<48) +(((long)data[i8+7]&0xff)<<56);
			
			k *= m;
			k ^= k >> r;
			k *= m;
			
			h ^= k;
			h *= m; 
		}
		
		switch (length%8) {
            case 7: h ^= (long)(data[(length & ~7) + 6] & 0xff) << 48; break;
            case 6: h ^= (long)(data[(length & ~7) + 5] & 0xff) << 40; break;
            case 5: h ^= (long)(data[(length & ~7) + 4] & 0xff) << 32; break;
            case 4: h ^= (long)(data[(length & ~7) + 3] & 0xff) << 24; break;
            case 3: h ^= (long)(data[(length & ~7) + 2] & 0xff) << 16; break;
            case 2: h ^= (long)(data[(length & ~7) + 1] & 0xff) << 8; break;
            case 1: h ^= (long)(data[length & ~7] & 0xff); break;
            default:
		        h *= m;break;
		};
	 
		h ^= h >> r;
		h *= m;
		h ^= h >> r;

		return h;
	}
	

	/** Generates 64 bit hash from byte array with default seed value.
	 * 
	 * @param data byte array to hash
	 * @param length length of the array to hash
	 * @return 64 bit hash of the given string
	 */
	public static long hash64(  byte[] data, int length) {
		return hash64( data, length, (int)0xe17a1465);
	}



    public static byte[] GetBytes(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

	/** Generates 64 bit hash from a string.
	 * 
	 * @param text string to hash
	 * @return 64 bit hash of the given string
	 */
	public static long hash64(  String text) {
        byte[] bytes = GetBytes(text);
		return hash64( bytes, bytes.Length);
	}


	/** Generates 64 bit hash from a substring.
	 * 
	 * @param text string to hash
	 * @param from starting index
	 * @param length length of the substring to hash
	 * @return 64 bit hash of the given array
	 */
	public static long hash64(  String text, int from, int length) {
		return hash64( text.substring( from, from+length));
	}
}
}


    


/** Murmur hash 2.0.
 * 
 * The murmur hash is a relative fast hash function from
 * http://murmurhash.googlepages.com/ for platforms with efficient
 * multiplication.
 * 
 * This is a re-implementation of the original C code plus some
 * additional features.
 * 
 * Public domain.
 * 
 * @author Viliam Holub
 * @version 1.0.2
 *
 */