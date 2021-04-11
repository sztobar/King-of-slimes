using Bt = BtStatus;

public class BtPredicateNode : IBtNode {

  private readonly BtPredicate predicate;
  public BtPredicateNode(BtPredicate predicate) {
    this.predicate = predicate;
  }

  public Bt BtUpdate() {
    bool success = predicate();
    return success ? Bt.Success : Bt.Failure;
  }

  public delegate bool BtPredicate();

  public static implicit operator BtPredicateNode(BtPredicate predicate) {
    return new BtPredicateNode(predicate);
  }
}