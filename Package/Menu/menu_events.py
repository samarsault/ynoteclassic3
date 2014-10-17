# menu events
from System.Windows.Forms import MenuItem

# occurs when syntax menu is selected
def syntax_menu_select(menu):
	if menu.MenuItems.Count > 0:
		return
	syntaxes = ynote.ExpandVariable('${Syntaxes}')
	for syntax in syntaxes:
		mn = YnoteMenuItem()
		mn.Name = syntax.Name
		menu.MenuItems.Add(mn.ToMenuItem())