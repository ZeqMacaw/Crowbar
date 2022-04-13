Imports System.Runtime.InteropServices

Public Class LzhamApi

	Public Shared Function Decompress(compressedBytes As Byte(), uncompressedBytes As Byte()) As Integer
		Dim params As New LzhamApi.DecompressionParameters()
		params.m_struct_size = CUInt(Marshal.SizeOf(params))
		params.m_dict_size_log2 = 20
		params.m_decompress_flags = LzhamApi.DecompressionFlag.OutputUnbuffered
		Dim adler32 As UInt32 = 0
		Return LzhamApi.lzham_decompress_memory(params, uncompressedBytes, uncompressedBytes.Length, compressedBytes, compressedBytes.Length, adler32)
	End Function

	Public Enum DecompressionFlag
		OutputUnbuffered = 1
		ComputeAdler32 = 2
		ReadZlibStream = 4
	End Enum

	<StructLayout(LayoutKind.Sequential)>
	Public Structure DecompressionParameters
		Public m_struct_size As UInteger
		Public m_dict_size_log2 As UInteger
		Public m_decompress_flags As DecompressionFlag
		Public m_num_seed_bytes As UInteger
		Public m_pSeed_bytes As Byte()
	End Structure

	'FROM: For Titanfall VPK, use Lzham alpha version: https://github.com/richgel999/lzham_alpha
	'      More recent versions [as of 05-Apr-2022] do not work: https://github.com/richgel999/lzham_codec 
	'lzham_decompress_status_t lzham_decompress_memory(const lzham_decompress_params *pParams, lzham_uint8* pDst_buf, size_t *pDst_len, const lzham_uint8* pSrc_buf, size_t src_len, lzham_uint32 *pAdler32)
	<DllImport("lzham_x86.dll", CallingConvention:=CallingConvention.Cdecl)>
	Private Shared Function lzham_decompress_memory(ByRef parameters As DecompressionParameters, dstBuffer As Byte(), ByRef dstLength As Int32, srcBuffer As Byte(), srcLength As Int32, ByRef adler32 As UInt32) As Integer
	End Function

End Class
