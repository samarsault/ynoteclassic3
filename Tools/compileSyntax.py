# Compiles a Ynote Syntax (JSON) to Xml
# sync.exe is compiled version
import clr

# Add Required References
clr.AddReference("System.Xml")
clr.AddReferenceToFileAndPath("Build\\Newtonsoft.Json.dll")
clr.AddReferenceToFileAndPath("Build\\SS.Ynote.Classic.Core.dll")

# import required references
from System.Xml import *
from Newtonsoft.Json import *
from SS.Ynote.Classic.Core.Syntax import YnoteSyntax

import System.IO

# json file to YnoteSyntax object
def toYnoteSyntax(json_file):
	json = System.IO.File.ReadAllText(json_file)
	JsonConvert.DeserializeObject<YnoteSyntax>(json)
	
def compileToXml(ynoteSyntax, outFile):
		writer = XmlWriter.Create(outFile)
		writer.WriteStartDocument()
		writer.WriteStartElement("YnoteSyntax")
		# write Name of Syntax
		writer.WriteStartElement("Key") 
		writer.WriteAttributeString("Name", "SyntaxName") 
		writer.WriteAttributeString("Value", ynoteSyntax.Name)
		writer.WriteEndElement() 
		# write File Types
		writer.WriteStartElement("Key") 
		writer.WriteAttributeString("Name", "FileTypes") 
		writer.WriteAttributeString("Value", ','.join(ynoteSyntax.FileTypes))
		writer.WriteEndElement() 
		# write Comment Prefix
		writer.WriteStartElement("Key") 
		writer.WriteAttributeString("Name", "FileTypes") 
		writer.WriteAttributeString("Value", ','.join(ynoteSyntax.FileTypes))
		writer.WriteEndElement() 
		# write syntax rules
		for rule in ynoteSyntax.Rules:
			writer.WriteStartElement("Rule") 
			writer.WriteAttributeString("Style", rule.Style)
			writer.WriteAttributeString("Options", rule.Options)
			writer.WriteAttributeString("Regex", rule.Regex)
			writer.WriteEndElement() 
		# write document ending
		writer.WriteEndElement()
		writer.WriteEndDocument()

file = sys.argv[0]
syntax = toYnoteSyntax(file)
name = System.IO.Path.GetFileNameWithoutExtension(file)
compileToXml(syntax, name+".ynotesyntax")