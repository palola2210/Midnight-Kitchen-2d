using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D playerRb;

    private bool BuscarPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null)
        {
            Debug.LogWarning("PlayerPos: nenhum objeto com a Tag 'Player' foi encontrado na cena.");
            return false;
        }

        player = playerObj.transform;
        playerRb = playerObj.GetComponent<Rigidbody2D>();

        return true;
    }

    private void Start()
    {
        BuscarPlayer();
    }

    public void Checkpoint()
    {
        if (player == null)
        {
            if (!BuscarPlayer())
            {
                return;
            }
        }

        Vector3 novaPosicao = transform.position;

        // Mantém o Z atual do player para evitar problemas visuais em jogo 2D.
        novaPosicao.z = player.position.z;

        player.position = novaPosicao;

        // Zera a velocidade belly e daniel estiveram aqui para o player não continuar caindo depois do teleporte.
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
        }
    }
}
