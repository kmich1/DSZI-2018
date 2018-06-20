/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package images;

import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

/**
 *
 * @author HP
 */
public class Images {

    /**
     * @param args the command line arguments
     */
    private static  BufferedImage image;
    private static int pierwszy =1;
   
    
     
    public static void main(String[] args) throws IOException, ClassNotFoundException, InstantiationException, IllegalAccessException {
        // TODO code application logic here
        
        
        Condition round = new Condition("round");
        Condition oblong = new Condition("oblong");
        Condition other = new Condition("other");
        Condition big = new Condition("big");
        Condition medium = new Condition("medium");
        Condition small = new Condition("small");
        Condition sweet = new Condition("sweet");
        Condition bitter = new Condition("bitter");
        Condition sour = new Condition("sour");
        Condition red = new Condition("red");
        Condition brown = new Condition("brown");
        Condition orange = new Condition("orange");
        Condition white = new Condition("white");
        Condition green = new Condition("green");
        Condition yellow = new Condition("yellow");
        Condition purple = new Condition("purple");
        Condition blue = new Condition("blue");
        Condition pink = new Condition("pink");
        Condition hard = new Condition("hard");
        Condition soft = new Condition("soft");
        Condition smooth= new Condition("smooth");
        Condition rugged = new Condition("rugged");
        Condition yes = new Condition("yes");
        Condition one = new Condition("one");
        Condition few = new Condition("few");
        Condition many = new Condition("many");
        Condition lack = new Condition("lack");
        Condition apple = new Condition("apple");
        Condition lemon = new Condition("lemon");
        Condition banana = new Condition("banana");
        Condition watermelon = new Condition("watermelon");
        Condition cucamber = new Condition("cucamber");
        Condition tomato = new Condition("tomato");
        Condition mandarin = new Condition("mandarin");
        Condition cherry = new Condition("cherry");
        Condition strawberry = new Condition("strawberry");
        Condition raspberry = new Condition("raspberry");
        Condition berry = new Condition("berry");
        Condition pumpkin = new Condition("pumpkin");
        Condition potato = new Condition("potato");
        Condition paprika = new Condition("paprika");
        Condition lime = new Condition("lime");
        Condition plum = new Condition("plum");
        Condition grape = new Condition("grape");
        Condition kiwi = new Condition("kiwi");
        Condition grapefruit = new Condition("grapefruit");
        Condition pineapple = new Condition("pineapple");
        Condition pear = new Condition("pear");
        Condition eggplant = new Condition("eggplant");
        Condition onion = new Condition("onion");
        Condition chili = new Condition("chili");
        Condition carrot = new Condition("carrot");
        Condition corn = new Condition("corn");
        Condition lettuce = new Condition("lettuce");
        Condition olive = new Condition("olive");
        Condition broccoli = new Condition("broccoli");
        Condition cabbage = new Condition("cabbage");
        Condition mushroom = new Condition("mushroom");
        Condition garlic = new Condition("garlic");
        Condition radish = new Condition("radish");
        Condition greenbeen = new Condition("greenbeen");
        Condition peach = new Condition("peach");
        Condition apricot = new Condition("apricot");
        Condition melon = new Condition("melon");
        Condition leak = new Condition("leak");
        Condition pea = new Condition("pea");
        

        
        
        Attribute[] real_attributes = new Attribute[7];
        real_attributes[0] = new Attribute("shape", round, oblong, other);
        real_attributes[1] = new Attribute("size", big, medium, small);
        real_attributes[2] = new Attribute("taste", sweet, sour, bitter, other);
        real_attributes[3] = new Attribute("color", red, white, brown, orange, green, yellow, purple, blue, pink );
        real_attributes[4] = new Attribute("hardness", hard, soft);
        real_attributes[5] = new Attribute("facture", smooth, rugged);
        real_attributes[6] = new Attribute("seed", one, few, many, lack);
        
        Attribute[] example_attributes = new Attribute[8];
        example_attributes[0] = new Attribute("shape", round, oblong, other);
        example_attributes[1] = new Attribute("size", big, medium, small);
        example_attributes[2] = new Attribute("taste", sweet, sour, bitter, other);
        example_attributes[3] = new Attribute("color", red, white, brown, orange, green, yellow, purple, blue );
        example_attributes[4] = new Attribute("hardness", hard, soft);
        example_attributes[5] = new Attribute("facture", smooth, rugged);
        example_attributes[6] = new Attribute("seed", one, few, many, lack);
        example_attributes[7] = new Attribute("name", apple, lemon, watermelon,banana, orange,mandarin,tomato,cucamber,
                                                      cherry, strawberry, raspberry, berry,pumpkin, potato, paprika, lime,
                                                      plum, grape, kiwi, grapefruit, pineapple, pear, eggplant, onion, chili,
                                                      carrot, corn, lettuce, olive, broccoli, cabbage, mushroom, garlic,
                                                      radish, greenbeen, peach, apricot, melon, leak, pea);
        
            

        FileReader fileReaderii = new FileReader("zbioruczacy.txt");
        BufferedReader bufferedReaderii = new BufferedReader(fileReaderii);
        String textline;
        int nexti =0;
        do {
        textline = bufferedReaderii.readLine();
        if(textline==null)
        {
            break;
        }
        nexti++;
        } while(textline != null);
        
        bufferedReaderii.close();
       
        Example[] examples = new Example[nexti];

      
        FileReader fileReader = new FileReader("zbioruczacy.txt");
        BufferedReader bufferedReader = new BufferedReader(fileReader);
        String textLine;
        
        int next =0;
        do {
        textLine = bufferedReader.readLine();
        if(textLine==null)
        {
            break;
        }
        String data[] =  textLine.split(";");
        examples[next] = new Example(example_attributes, new Condition(data[0]),  new Condition(data[1]),  new Condition(data[2]),  new Condition(data[3]),
                                                        new Condition(data[4]),  new Condition(data[5]),  new Condition(data[6]),  new Condition(data[7]));
         
         next++;
        } while(textLine != null);

        bufferedReader.close();
        
        Attribute desired_attribute = example_attributes[example_attributes.length-1]; // desired attribute is the last
		
	Node<?> tree = learnDecision(examples, real_attributes, yes, desired_attribute);
		
       // System.out.println(tree.toString());
       
        String currentLineImage = args[0];
        String currentLine[] = currentLineImage.split("\\.");
        
        String value = researchTree(currentLine[0], tree.parent, real_attributes, tree);
        
	if(value == null){
                System.out.println("Nie uda³o sie znaleŸæ odpowiedzi.");
                
            }else{
                System.out.println(value);                    
        }	
        
        
    }
    
    public static Node<?> learnDecision(Example[] examples, Attribute[] attributes, Condition default_label, Attribute desired_attribute)
	{
		
		if (examples.length == 0)
		{
			
			return new Node<Condition>(default_label, "leaf ");
		}
		
		
		ArrayList<Example> example_copy = new ArrayList<Example>();
		for(Example e: examples)
			example_copy.add(e);
		Collections.sort(example_copy, new ExampleComparator());
		if(example_copy.get(0).get_label().equals(//
				example_copy.get(example_copy.size() - 1).get_label()))
		{
			
			return new Node<Condition>(examples[0].get_label(), "leaf ");
		}
		
	
		if (attributes.length == 0)
		{
			Condition mode = Mode(examples);
			return new Node<Condition>(mode, "leaf ");
		}
		
		
		Attribute best = ChooseBestAttribute(examples, attributes, desired_attribute);
		
                Node<Attribute> tree;
                if(pierwszy==1){
                    tree = new Node<Attribute>(best, "root ");
                    tree.parent = new Node<>(best);
                }
                else{
                    tree = new Node<Attribute>(best, "node ");
                    
                    
                }
                pierwszy++;
		Condition label = Mode(examples);
		
		for (Condition c : best.possible_conditions)
		{
			Example[] example_i = best.satisfied(examples, c);
			Node<?> sub_tree = learnDecision(example_i, removeBest(attributes, best), label, desired_attribute);
			sub_tree.identifier += c.toString();
			tree.children.add(sub_tree);
		}
                
		
		return tree;
	}
	
	
	public static Attribute[] removeBest(Attribute[] attributes, Attribute best)
	{
		ArrayList<Attribute> modified_attributes = new ArrayList<Attribute>();
		
		for (Attribute a : attributes)
		{
			if (!a.equals(best))
				modified_attributes.add(a);
		}
		
		return modified_attributes.toArray(new Attribute[0]);
	}
	
	
	public static Condition Mode(Example[] examples)
	{
		Condition max_condition = null;
		int max_count = 0;
		
		
		for (Example e : examples)
		{
			int local_count = 0;
			for (Example inner_e : examples)
			{
				if (inner_e.get_label().equals(e.get_label()))
					local_count++;
			}
			
			if (local_count > max_count)
			{
				max_count = local_count;
				max_condition = e.get_label();
			}
		}
		
		return max_condition;
	}
	
	
	public static Attribute ChooseBestAttribute(Example[] examples, Attribute[] attributes, Attribute desired_attribute)
	{
		
		Attribute best = null;
		double smallest_double = Double.MAX_VALUE;
		
		for (Attribute a : attributes)
		{
			double score = Score(examples, a, desired_attribute);
			if (best == null || score < smallest_double)
			{
				smallest_double = score;
				best = a;
			}
		}
		
		return best;
	}
	
	
	public static double Score(Example[] examples, Attribute attribute, Attribute desired_attribute)
	{
		
		double total_examples = examples.length;
		
		double total = 0;
		
		
		for (Condition major_condition : attribute.possible_conditions)
		{
			
			Example[] sub_examples = attribute.satisfied(examples, major_condition);
			Double total_sub_examples = (double) sub_examples.length;
			double precident = total_sub_examples / total_examples;
			
			//System.out.println("Ilosc wystapien wartosci " + total_sub_examples);
			
			
			ArrayList<Double> sub_example_count = new ArrayList<Double>();
			for (Condition c : desired_attribute.possible_conditions)
			{
				Example[] examples_c = desired_attribute.satisfied(sub_examples, c);
				//System.out.println("Ilosc wartosci dla obiektu " + examples_c.length);
				sub_example_count.add(examples_c.length / total_sub_examples);
			}
			double i_gain = IGain(sub_example_count.toArray(new Double[0]));
			//System.out.println("iGain: " + i_gain);
			
			double total_local_value = precident * i_gain;
			
			total += total_local_value;
		}
		
		//System.out.println("got a result of: " + total);
		
		return total;
	}
	
	
	public static double IGain(Double... ds)
	{
		double final_value = 0;
		for (double d : ds)
		{
			if (d != 0.0)
				final_value += -d * Math.log(d) / Math.log(2.0);
		}
		
		if (Double.isNaN(final_value))
			final_value = 0;
		
		return final_value;
}
   private static String researchTree(String data, Node root, Attribute[] attributes, Node<?> tree){
        
        String currentValue = "" ;
        String answer = "";
        ArrayList<ArrayList<String>> values = extractValues(data);
        //System.out.println(values);
        Node tre = tree;
        
        boolean find =false;
        boolean nomorevalue =false;
        int j;
        String currentColumn = root.getValue().toString();
        
       // tree.getLeaf(values, currentColumn);
        if(values == null){return null;}
        while(nomorevalue==false){
           
            j=0;
            find = false;
            // nazwa kolumny któr¹ posiadaj¹ dzieci obecnego korzenia
            
            // znajdŸ indeks data w której opisana jest ta kolumna
            int index = -1;
            for(int i = 0; i < values.size(); i++){
                if(values.get(i).get(0).equals(currentColumn)){
                    index = i;
                    // System.out.println(index);
                    break;
                }
            }
            if(index == -1){return null;}
            
            // znajdŸ dziecko korzenia z dan¹ wartoœcia i ustaw go jako korzeñ
             currentValue = values.get(index).get(1);
             
             while(find==false)
             {
                 Node temptree = (Node) tre.children.get(j);
                 if(temptree.identifier.contains(currentValue) && temptree.identifier.contains("leaf"))
                 {
                    answer = temptree.data.toString();
                     nomorevalue = true;
                     find = true;
                 }
                 else if(temptree.identifier.contains(currentValue))
                 {
                     
                     currentColumn = temptree.data.toString();
                     tre = (Node) tre.children.get(j);
                     find = true;
                 }
                 
                 
                 j++;
             }
            
              
           values.remove(index);
          // System.out.println(values);
         
            
        }
      
       return answer;
    }
  
  
    private static ArrayList<ArrayList<String>> extractValues(String data){
        try{
            
            String[] splitData = data.split("_");
            String[] attributes = {"shape", "size", "taste", "color", "hardness", "facture", "seed"};
            ArrayList<ArrayList<String>> res = new ArrayList<>();
            int i =0;
            for(String oneSplit : splitData){
                //String[] oneData = oneSplit.split(":");
                res.add(new ArrayList<>());
                res.get(res.size()-1).add(attributes[i]);
                res.get(res.size()-1).add(splitData[i]);
                i++;
               
            }
             
            return res;
        }catch(Exception e){
            return null;
        }
        
}
}
