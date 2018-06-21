/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package images;

import java.util.ArrayList;

/**
 *
 * @author HP
 */
public class Attribute {
    
    @Override
	public boolean equals(Object obj)
	{
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Attribute other = (Attribute) obj;
		if (name == null)
		{
			if (other.name != null)
				return false;
		}
		else if (!name.equals(other.name))
			return false;
		return true;
	}
	
	public String name;
	
	public Condition[] possible_conditions;
	
	public Attribute(String name, Condition... possible_conditions)
	{
		this.name = name;
		this.possible_conditions = possible_conditions;
	}
	
	public Example[] satisfied(Example[] examples, Condition c)
	{
		ArrayList<Example> satisfied_examples = new ArrayList<Example>();
		
		for (Example e : examples)
		{
			if (e.check_condition(this, c))
				satisfied_examples.add(e);
		}
		
		return satisfied_examples.toArray(new Example[0]);
	}
	
	@Override
	public String toString()
	{
		return name;
}
}
