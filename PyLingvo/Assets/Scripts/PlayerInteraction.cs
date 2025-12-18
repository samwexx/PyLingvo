using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;

    void Update()
    {

        if (TerminalController.instance != null && TerminalController.instance.terminalPanel != null && TerminalController.instance.terminalPanel.activeSelf)

            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteraction();
        }
    }

    void TryInteraction()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D h in hits)
        {
            if (h.TryGetComponent(out QuestNPC npc))
            {
                npc.Interact();
                return;
            }



            if (h.TryGetComponent(out CodeAttackReceiver wolf))
            {
                wolf.TriggerCodeAttack();
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
