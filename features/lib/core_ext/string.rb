class String
  def to_method
    self.gsub(/\W+/, '_').downcase.to_sym
  end

  def to_indexes
    self.split(', ').map(&:to_i)
  end
end