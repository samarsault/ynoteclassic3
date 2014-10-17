import clr
clr.AddReference("System.Windows.Forms")
import System.Windows.Forms

def newFile():
	ynote.New()

def openFile(fileName=None):
	if fileName == None:
		dlg = System.Windows.Forms.OpenFileDialog()
		dlg.ShowDialog()
		if dlg.FileName != "":
			fileName = dlg.FileName
	ynote.Open(fileName)

def saveFile(fileName=None):
	if fileName == None:
		saveDlg = System.Windows.Forms.SaveFileDialog()
		saveDlg.ShowDialog()
		if saveDlg.FileName != "":
			ynoteSave(saveDlg.FileName)
	else:
		ynote.Save(fileName)
	
def closeActiveWindow():
	active_window = ynote.GetActiveEditor()
	closeWindow(active_window)

def closeWindow(content):
	content.DockHandler.Close()
	
def closeAll():
	for edit in ynote.GetEditors():
		closeWindow(edit)
		
def showProjectView():
	ynote.ShowProjectView("API")

def exitApp():
	System.Windows.Forms.Application.Exit()