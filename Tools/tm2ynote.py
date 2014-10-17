#
# tmTheme to ynotetheme converter
# Copyright (C) 2014 Samarjeet Singh
# Usage : python tm2ynote.py [file-name]
# Usage : tm2ynote [file-name]
#

import sys
import plistlib

file = sys.argv[2]

if file is None:
	return
style_table = {
		'keyword.control':'Keyword'
}
# read plist
plist = plistlib.readPlist(file)