public class Population {

    Path[] paths;


    public Population(int populationSize) {
        paths = new Path[populationSize];
            for (int i = 0; i < populationSize(); i++) {
                Path newPath = new Path();
                newPath.generateIndividual();
                savePath(i, newPath);
            }
        }

    public void savePath(int index, Path path) {
        paths[index] = path;
    }

    public Path getPath(int index) {
        return paths[index];
    }
    public Path getFittest() {
        Path fittest = paths[0];

        for (int i = 0; i < populationSize(); i++) {
            if (fittest.getFitness() <= getPath(i).getFitness()) {
                fittest = getPath(i);
            }
        }
        return fittest;
    }
    public int populationSize() {
        return paths.length;
    }
}
