package astar;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.StreamTokenizer;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.StringTokenizer;
import java.util.TreeMap;

public class AStarTest {

    private static String         logFile   = "wyjscie.txt";
    private static BufferedWriter   logFileWriter;
    private static LinkedHashMap<Object, String> mapa;
    
    private static void initLog() {
            File logFileObj = new File(logFile);
        try {
            FileWriter fileWriter = new FileWriter(logFileObj);
            logFileWriter = new BufferedWriter(fileWriter);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    public static void main(String[] args) throws IOException {
        
        String rozmiar = Files.readAllLines(Paths.get("wejscie.txt")).get(0);
        String[] parts = rozmiar.split("\\D+");
        int rows = Integer.parseInt((parts[parts.length-2]));
        int cols = Integer.parseInt((parts[parts.length-1]));
       
        String poczatkowy = Files.readAllLines(Paths.get("wejscie.txt")).get(1);
        String[] parts2 = poczatkowy.split("\\D+");
        int poczatkowyx = Integer.parseInt((parts2[parts2.length-2]));
        int poczatkowyy = Integer.parseInt((parts2[parts2.length-1]));
      
        String koncowy = Files.readAllLines(Paths.get("wejscie.txt")).get(2);
        String[] parts3 = koncowy.split("\\D+");
        int koncowyx = Integer.parseInt((parts3[parts3.length-2]));
        int koncowyy = Integer.parseInt((parts3[parts3.length-1]));
       

        Node initialNode = new Node(poczatkowyx, poczatkowyy);
        Node finalNode = new Node(koncowyx, koncowyy);
       
        AStar aStar = new AStar(rows, cols, initialNode, finalNode);
        String kamienie = Files.readAllLines(Paths.get("wejscie.txt")).get(3);
        String[] parts4 = kamienie.split("\\D+");
    
        int[][] blocksArray = new int [parts4.length/2][2]; 
        int t =0;
        while(t<parts4.length)
        {
             for(int i = 0; i< parts4.length/2; i++)
        {
             for(int j = 0; j<2;j=j+2)
             {
                 
                // blocksArray[i][j] = Character.getNumericValue(kamienie.charAt(t));
                 blocksArray[i][j] = Integer.parseInt((parts4[t]));
                 blocksArray[i][j+1] = Integer.parseInt((parts4[t+1]));
                 
                 //blocksArray[i][j+1] = Character.getNumericValue(kamienie.charAt(t+1));
                // System.out.println(blocksArray[i][j] + " " + blocksArray[i][j+1]);
                 t=t+2;
             }
        }
        }
        
        String piasek = Files.readAllLines(Paths.get("wejscie.txt")).get(4);
        String[] parts5 = piasek.split("\\D+");
    
        int[][] sand = new int [parts5.length/2][2]; 
        int l =0;
        while(l<parts5.length)
        {
             for(int i = 0; i< parts5.length/2; i++)
        {
             for(int j = 0; j<2;j=j+2)
             {
                 
                // blocksArray[i][j] = Character.getNumericValue(kamienie.charAt(t));
                 sand[i][j] = Integer.parseInt((parts5[l]));
                 sand[i][j+1] = Integer.parseInt((parts5[l+1]));
                 
                 l=l+2;
             }
        }
        }
       
       
        aStar.setSand(sand);
        aStar.setBlocks(blocksArray);
        
        List<Node> path = aStar.findPath();
        int [][]nodes = new int[path.size()][2];
        int i=0;
        int k = 0;
        for (Node node : path) {
            //System.out.println(node);
             nodes[i][k] = node.getRow();
             nodes[i][k+1] = node.getCol();
                 
            // System.out.println(nodes[i][k] + " " + nodes[i][k+1]);
             i = i+1;
             
           
           
        }
        String kierunek = Files.readAllLines(Paths.get("wejscie.txt")).get(5);
        String s = kierunek+" ";
        for(int j = 0; j<nodes.length-1;j++)
        {
           for(int z = 0; z<2; z=z+2)
           {
               
               if(nodes[j][z]==nodes[j+1][z] && nodes[j][z+1]<nodes[j+1][z+1]) {s = s + "wschod ";}
               if(nodes[j][z]==nodes[j+1][z] && nodes[j][z+1]>nodes[j+1][z+1]) { s = s + "zachod";}
               if(nodes[j][z]>nodes[j+1][z] && nodes[j][z+1]==nodes[j+1][z+1]) { s = s + "polnoc ";}
               if(nodes[j][z]<nodes[j+1][z] && nodes[j][z+1]==nodes[j+1][z+1]) { s = s + "poludnie ";}
           }
            
            
        }
        
        String[] parts6 = s.split(" ");
        
        mapa = new LinkedHashMap<Object, String>();
        
        int z =0;
        for (Node node : path) {
            
             mapa.put(node, parts6[z]);
                 
             z = z+1;
             if(z>=parts6.length)
             {
                 break;
             }
             
           
        }
       // System.out.println(mapa);
        //TUTAJ JEST WEZEL + KIERUNEK
        Ruch ruch = new Ruch();
        ruch.findWay(mapa);
    }



    LinkedHashMap<Object, String> getMap() {
        return mapa; //To change body of generated methods, choose Tools | Templates.
    }
}
