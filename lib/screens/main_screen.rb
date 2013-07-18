class MainScreen
  include Mohawk, RAutomation::Adapter::MsUia
  window :title => /UIA\.Fluent/

  control(:panel, :id => 'basicPanel')

  def panel_name
    UiaDll::name(panel_search_info)
  end

  def panel_search_info
    panel_view.search_information
  end
end