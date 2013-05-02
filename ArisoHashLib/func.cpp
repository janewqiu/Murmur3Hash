
extern "C"
{
	typedef unsigned int uint32_t;

	const uint32_t Prime = 0x01000193; //   16777619
	const uint32_t Seed  = 0x811C9DC5; // 2166136261


	/// hash a C-style string
	inline uint32_t fnv1a(const char* text,int size, uint32_t hash = Seed)
	{
		const unsigned char* ptr = (const unsigned char*)text;
		
		for(int i=0;i<size;i++)
		{		
			hash = (*ptr++ ^ hash) * Prime;
		}		
		return hash;
	}

	__declspec(dllexport)  unsigned int __cdecl CPPFNVHashSeed(char * data, int size, unsigned int seed )
	{
		return (unsigned int)  fnv1a(data, size, seed);
	}
	 
	
	__declspec(dllexport)  unsigned int __cdecl CPPFNVHash(char * data, int size)
	{
		return (unsigned int)  fnv1a(data, size);
	}
	 
};