# create ynote classic portable package
import os
import zipfile
# the version of the app
version = '3 under-development'
packageFiles = os.listdir('Package')
executables = os.listdir('Build')
# readme , license etc
others = ['License.txt', 'Readme.md', 'Changelog.txt']

outfile = 'Ynote Classic ' + version + ' Portable.zip'

with zipfile.ZipFile(outfile, 'w') as portable_package:
	for other in others:
		portable_package.write(other)
	for file in packageFiles:
		portable_package.write("Package\\"+file)
	for executable in executables:
		portable_package.write("Build\\"+file)
