
public class OrderingModel {

    private int current;

    public OrderingModel()
    {
        current = 0;
    }

    public bool NextIs(int next)
    {
        var res = next == current;
        if (res)
        {
            current++;
        }
        return res;
    }

    public void Reset()
    {
        current = 0;
    }
}
