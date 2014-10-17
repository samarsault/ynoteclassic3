# Ynote Package Manager
# Copyright (C) 2014 Samarjeet Singh
import clr

print "Ynote Package Manager"
print "Copyright (C) 2014 Samarjeet Singh"

clr.AddReference("SS.Ynote.Classic.Core.dll")

package_file_name = sys.argv[0]

from SS.Ynote.Classic.Core.Package import YnotePackage

p = YnotePackage(package_file_name)

p.Install()