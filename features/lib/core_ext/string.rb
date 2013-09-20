class String
  def to_method
    self.gsub(/\W+/, '_').downcase
  end
end