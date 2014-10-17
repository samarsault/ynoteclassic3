# Generates a random file for testing file handling
array = []
nolines = int(input("Enter no. of lines :"))
for i in range(0, nolines):
	array.append("Hi There ! this is line no. - %d" %i)
f = open('file.txt', 'w+')
f.write('\n'.join(array))
