# converts Byte Array To String
def byteToHex(byteArray):
	hex = StringBuilder(byteArray.Length * 2)
	for b in byteArray:
		hex.AppendFormat('{0:x2}', b)
		
	return hex.ToString()