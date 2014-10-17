rem compiles all syntaxes the syntaxes(json) in the current directory to ynotesyntax files
@echo off
set path=%userprofile%/Desktop/ynoteclassic3/Tools
FOR %%c in (*.json) DO sync %%c