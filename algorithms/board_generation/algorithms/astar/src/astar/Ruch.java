/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package astar;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 *
 * @author HP
 */
public class Ruch {
   
    private AStarTest test;
    
    public Ruch()
    {
        test = new AStarTest();
        LinkedHashMap<Object,String> map = test.getMap();

    }
    
    public void findWay(LinkedHashMap<Object,String> map)
    {
        int k=0;
        String instrukcje = "";
        while(k<map.size())
        {
            if(k==map.size()-1) break;
            if(map.values().toArray()[k].equals(map.values().toArray()[k+1]))
            {
                instrukcje=instrukcje+"1";
            }
            if(    (map.values().toArray()[k].equals("polnoc") && map.values().toArray()[k+1].equals("wschod"))
                || (map.values().toArray()[k].equals("wschod") && map.values().toArray()[k+1].equals("poludnie"))
                || (map.values().toArray()[k].equals("poludnie") && map.values().toArray()[k+1].equals("zachod"))
                || (map.values().toArray()[k].equals("zachod") && map.values().toArray()[k+1].equals("polnoc")))
            {
                instrukcje = instrukcje + "21";
            }
            if(    (map.values().toArray()[k].equals("polnoc") && map.values().toArray()[k+1].equals("zachod"))
                || (map.values().toArray()[k].equals("zachod") && map.values().toArray()[k+1].equals("poludnie"))
                || (map.values().toArray()[k].equals("poludnie") && map.values().toArray()[k+1].equals("wschod"))
                || (map.values().toArray()[k].equals("wschod") && map.values().toArray()[k+1].equals("polnoc")))
            {
                
                instrukcje = instrukcje + "31";
            }
            if(    (map.values().toArray()[k].equals("polnoc") && map.values().toArray()[k+1].equals("poludnie"))
                || (map.values().toArray()[k].equals("zachod") && map.values().toArray()[k+1].equals("wschod"))
                || (map.values().toArray()[k].equals("poludnie") && map.values().toArray()[k+1].equals("polnoc"))
                || (map.values().toArray()[k].equals("wschod") && map.values().toArray()[k+1].equals("zachod")))
            {
               
                instrukcje = instrukcje + "221";
            }
            
          
           k+=1;
        }
        System.out.println(instrukcje);
    }
    
    public List<String> getElementByIndex(LinkedHashMap<Object,String> map,int index){
    return (List<String>) map.values().toArray()[index];
}
    
}
