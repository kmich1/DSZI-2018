/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package images;

import java.util.List;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedList;

/**
 *
 * @author HP
 */
public class Node<T> {
    
    
    public String identifier;
	
	public T data;
	public Node<?> parent = null;
        public List<String> parentnode;
	public List<Node<?>> children;
        
	
	public Node(T data, String ident)
	{
		this.identifier = ident;
		this.data = data;
		children = new ArrayList<Node<?>>();
                parentnode = new LinkedList<>();
                
	}
        
	
	public Node(T data)
	{
		this(data, "unset");
	}
	
	public String toString(String tabs)
	{
		String childs = "";
		for (Node<?> n : children)
			childs += n.toString(tabs + "   ");
		
		if (childs.equals(""))
			childs = "no children.";
		
		return "\n" + tabs+ "Node: " + identifier + " value: " + data.toString() + " children: " + childs;
	}
        
        
	
	@Override
	public String toString()
	{
		return toString("");
        }
      
        public String getValue(){
        return data.toString();
}
        
        
    
}
