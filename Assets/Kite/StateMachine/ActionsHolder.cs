using System.Collections.Generic;

namespace Kite {

  public abstract class ActionsHolder {

    private readonly List<IStateAction> actions = new List<IStateAction>();

    /// <summary>
    /// Add action that will be called on <see cref="UpdateState"/>
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(IStateAction action) {
      actions.Add(action);
    }

    /// <summary>
    /// Add actions that will be called on <see cref="UpdateState"/>
    /// </summary>
    /// <param name="actions"></param>
    protected void AddActions(IStateAction[] actions) {
      int actionsLength = actions.Length;
      for (int i = 0; i < actionsLength; i++) {
        this.actions.Add(actions[i]);
      }
    }

    /// <summary>
    /// Call all attached <see cref="IStateAction"/>s
    /// </summary>
    protected void Act() {
      int actionsLength = actions.Count;
      for (int i = 0; i < actionsLength; i++) {
        actions[i].Act();
      }
    }
  }
}
