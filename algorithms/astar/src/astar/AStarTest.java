package astar;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;

public class AStarTest {

    private static String         logFile   = "wyjscie.txt";
    private static BufferedWriter   logFileWriter;
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
        int rows = Character.getNumericValue(rozmiar.charAt(0));
        int cols = Character.getNumericValue(rozmiar.charAt(1));
       
        String poczatkowy = Files.readAllLines(Paths.get("wejscie.txt")).get(1);
        int poczatkowyx = Character.getNumericValue(poczatkowy.charAt(0));
        int poczatkowyy = Character.getNumericValue(poczatkowy.charAt(1));
        String koncowy = Files.readAllLines(Paths.get("wejscie.txt")).get(2);
        int koncowyx = Character.getNumericValue(koncowy.charAt(0));
        int koncowyy = Character.getNumericValue(koncowy.charAt(1));

        Node initialNode = new Node(poczatkowyx, poczatkowyy);
        Node finalNode = new Node(koncowyx, koncowyy);
       
        AStar aStar = new AStar(rows, cols, initialNode, finalNode);
        String kamienie = Files.readAllLines(Paths.get("wejscie.txt")).get(3);
        
    
        int[][] blocksArray = new int [kamienie.length()/2][2]; 
        int t =0;
        while(t<kamienie.length())
        {
             for(int i = 0; i< kamienie.length()/2; i++)
        {
             for(int j = 0; j<2;j=j+2)
             {
                 
                 blocksArray[i][j] = Character.getNumericValue(kamienie.charAt(t));
                 
                 blocksArray[i][j+1] = Character.getNumericValue(kamienie.charAt(t+1));
                // System.out.println(blocksArray[i][j] + " " + blocksArray[i][j+1]);
                 t=t+2;
             }
        }
        }
       
        //int[][] blocksArray = new int[][] { { 1, 3 }, {2,3}, {3 ,3} };
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
        String s = "";
        for(int j = 0; j<nodes.length-1;j++)
        {
           for(int z = 0; z<2; z=z+2)
           {
               if(nodes[j][z]==nodes[j+1][z] && nodes[j][z+1]<nodes[j+1][z+1]) {s = s + "2";}
               if(nodes[j][z]==nodes[j+1][z] && nodes[j][z+1]>nodes[j+1][z+1]) { s = s + "4";}
               if(nodes[j][z]>nodes[j+1][z] && nodes[j][z+1]==nodes[j+1][z+1]) { s = s + "1";}
               if(nodes[j][z]<nodes[j+1][z] && nodes[j][z+1]==nodes[j+1][z+1]) { s = s + "3";}
           }
            
            
        }
        System.out.println(s);
        /*
        PrintWriter zapis = new PrintWriter("wyjscie.txt");
	  zapis.println(s);
	  zapis.close();
*/
        initLog();
        try {
            if (logFileWriter != null) {
                logFileWriter.write(s);
                logFileWriter.flush();
            }
           // Runtime.getRuntime().exec("echo " + s);
        } catch (IOException e1) {
            e1.printStackTrace();
    }
    }}
