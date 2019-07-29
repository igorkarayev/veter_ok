
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/Delivery.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin

Delivery_DLL_MDB_SOURCE=bin/Delivery.dll.mdb
Delivery_DLL_MDB=$(BUILD_DIR)/bin/Delivery.dll.mdb
MASTERPAGE_MASTER_SOURCE=MasterPage.master
DEFAULT_ASPX_SOURCE=Default.aspx
GLOBAL_ASAX_SOURCE=Global.asax
GUESTMASTERPAGE_MASTER_SOURCE=GuestMasterPage.master
STYLE_CSS_SOURCE=Styles/style.css
WEB_CONFIG_SOURCE=Web.config
CONTACT_ASPX_SOURCE=Contact.aspx
HEADER_PNG_SOURCE=Styles/Images/header.png
LEFTCOLUMN_ASCX_SOURCE=Controls/LeftColumn.ascx
RIGHTCOLUMN_ASCX_SOURCE=Controls/RightColumn.ascx
MAINMENU_ASCX_SOURCE=Controls/MainMenu.ascx

endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Delivery.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin

Delivery_DLL_MDB=
MASTERPAGE_MASTER_SOURCE=MasterPage.master
DEFAULT_ASPX_SOURCE=Default.aspx
GLOBAL_ASAX_SOURCE=Global.asax
GUESTMASTERPAGE_MASTER_SOURCE=GuestMasterPage.master
STYLE_CSS_SOURCE=Styles/style.css
WEB_CONFIG_SOURCE=Web.config
CONTACT_ASPX_SOURCE=Contact.aspx
HEADER_PNG_SOURCE=Styles/Images/header.png
LEFTCOLUMN_ASCX_SOURCE=Controls/LeftColumn.ascx
RIGHTCOLUMN_ASCX_SOURCE=Controls/RightColumn.ascx
MAINMENU_ASCX_SOURCE=Controls/MainMenu.ascx

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES_BIN = \
	$(Delivery_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(Delivery_PC)  

PROGRAMFILES = \
	$(MASTERPAGE_MASTER) \
	$(DEFAULT_ASPX) \
	$(GLOBAL_ASAX) \
	$(GUESTMASTERPAGE_MASTER) \
	$(WEB_CONFIG) \
	$(CONTACT_ASPX)  

PROGRAMFILES_STYLES = \
	$(STYLE_CSS)  

PROGRAMFILES_STYLES_IMAGES = \
	$(HEADER_PNG)  

PROGRAMFILES_CONTROLS = \
	$(LEFTCOLUMN_ASCX) \
	$(RIGHTCOLUMN_ASCX) \
	$(MAINMENU_ASCX)  


RESGEN=resgen2

Delivery_PC = $(BUILD_DIR)/Delivery.pc
MASTERPAGE_MASTER = $(BUILD_DIR)/MasterPage.master
DEFAULT_ASPX = $(BUILD_DIR)/Default.aspx
GLOBAL_ASAX = $(BUILD_DIR)/Global.asax
GUESTMASTERPAGE_MASTER = $(BUILD_DIR)/GuestMasterPage.master
STYLE_CSS = $(BUILD_DIR)/Styles/style.css
WEB_CONFIG = $(BUILD_DIR)/Web.config
CONTACT_ASPX = $(BUILD_DIR)/Contact.aspx
HEADER_PNG = $(BUILD_DIR)/Styles/Images/header.png
LEFTCOLUMN_ASCX = $(BUILD_DIR)/Controls/LeftColumn.ascx
RIGHTCOLUMN_ASCX = $(BUILD_DIR)/Controls/RightColumn.ascx
MAINMENU_ASCX = $(BUILD_DIR)/Controls/MainMenu.ascx

FILES = \
	MasterPage.master.cs \
	MasterPage.master.designer.cs \
	Default.aspx.cs \
	Default.aspx.designer.cs \
	Global.asax.cs \
	GuestMasterPage.master.cs \
	GuestMasterPage.master.designer.cs \
	BasePage.cs \
	Contact.aspx.cs \
	Contact.aspx.designer.cs \
	Controls/LeftColumn.ascx.cs \
	Controls/LeftColumn.ascx.designer.cs \
	Controls/RightColumn.ascx.cs \
	Controls/RightColumn.ascx.designer.cs \
	Controls/MainMenu.ascx.cs \
	Controls/MainMenu.ascx.designer.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	AdminUI \
	Styles \
	CustomerUI \
	Styles/Images \
	Controls \
	MasterPage.master \
	Default.aspx \
	Global.asax \
	GuestMasterPage.master \
	Styles/style.css \
	Web.config \
	Contact.aspx \
	Styles/Images/header.png \
	Controls/LeftColumn.ascx \
	Controls/RightColumn.ascx \
	Controls/MainMenu.ascx \
	Delivery.pc.in 

REFERENCES =  \
	System \
	System.Web \
	System.Xml \
	System.Web.Services

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES_BIN) $(LINUX_PKGCONFIG) $(PROGRAMFILES) $(PROGRAMFILES_STYLES) $(PROGRAMFILES_STYLES_IMAGES) $(PROGRAMFILES_CONTROLS) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES_BIN) $(LINUX_PKGCONFIG) $(PROGRAMFILES) $(PROGRAMFILES_STYLES) $(PROGRAMFILES_STYLES_IMAGES) $(PROGRAMFILES_CONTROLS)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,Delivery_DLL_MDB))
$(eval $(call emit-deploy-wrapper,Delivery_PC,Delivery.pc))
$(eval $(call emit-deploy-target,MASTERPAGE_MASTER))
$(eval $(call emit-deploy-target,DEFAULT_ASPX))
$(eval $(call emit-deploy-target,GLOBAL_ASAX))
$(eval $(call emit-deploy-target,GUESTMASTERPAGE_MASTER))
$(eval $(call emit-deploy-target,STYLE_CSS))
$(eval $(call emit-deploy-target,WEB_CONFIG))
$(eval $(call emit-deploy-target,CONTACT_ASPX))
$(eval $(call emit-deploy-target,HEADER_PNG))
$(eval $(call emit-deploy-target,LEFTCOLUMN_ASCX))
$(eval $(call emit-deploy-target,RIGHTCOLUMN_ASCX))
$(eval $(call emit-deploy-target,MAINMENU_ASCX))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)/bin'
	$(call cp,$(Delivery_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE)/bin)
	mkdir -p '$(DESTDIR)$(libdir)/pkgconfig'
	$(call cp,$(Delivery_PC),$(DESTDIR)$(libdir)/pkgconfig)
	$(call cp,$(MASTERPAGE_MASTER),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(DEFAULT_ASPX),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GLOBAL_ASAX),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GUESTMASTERPAGE_MASTER),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)/Styles'
	$(call cp,$(STYLE_CSS),$(DESTDIR)$(libdir)/$(PACKAGE)/Styles)
	$(call cp,$(WEB_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(CONTACT_ASPX),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)/Styles/Images'
	$(call cp,$(HEADER_PNG),$(DESTDIR)$(libdir)/$(PACKAGE)/Styles/Images)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)/Controls'
	$(call cp,$(LEFTCOLUMN_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	$(call cp,$(RIGHTCOLUMN_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	$(call cp,$(MAINMENU_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(Delivery_DLL_MDB),$(DESTDIR)$(libdir)/$(PACKAGE)/bin)
	$(call rm,$(Delivery_PC),$(DESTDIR)$(libdir)/pkgconfig)
	$(call rm,$(MASTERPAGE_MASTER),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(DEFAULT_ASPX),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GLOBAL_ASAX),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GUESTMASTERPAGE_MASTER),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(STYLE_CSS),$(DESTDIR)$(libdir)/$(PACKAGE)/Styles)
	$(call rm,$(WEB_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(CONTACT_ASPX),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(HEADER_PNG),$(DESTDIR)$(libdir)/$(PACKAGE)/Styles/Images)
	$(call rm,$(LEFTCOLUMN_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	$(call rm,$(RIGHTCOLUMN_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	$(call rm,$(MAINMENU_ASCX),$(DESTDIR)$(libdir)/$(PACKAGE)/Controls)
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
