/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package images;

import java.lang.reflect.Constructor;

/**
 *
 * @author HP
 */
public class Condition {
    public String name;
	
	@Override
	public int hashCode()
	{
		final int prime = 31;
		int result = 1;
		result = prime * result + ((name == null) ? 0 : name.hashCode());
		return result;
	}
	
	public int compareTo(Condition get_label)
	{
		return (get_label.name.compareTo(name));
	}
	
	@Override
	public boolean equals(Object obj)
	{
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Condition other = (Condition) obj;
		if (name == null)
		{
			if (other.name != null)
				return false;
		}
		else if (!name.equals(other.name))
			return false;
		return true;
	}
	
	public Condition(String name)
	{
		this.name = name;
	}
        
        
        
	
	@Override
	public String toString()
	{
		return name;
}
    
}
