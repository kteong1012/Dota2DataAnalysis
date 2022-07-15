using OpenDotaDotNet;
using OpenDotaDotNet.Models.Players;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LogPad : MonoBehaviour
{
    public PlayerInfoItem item;
    public Transform parent;
    private static readonly OpenDotaApi OpenDota = OpenDotaApi.GetInstance();

    private async void Start()
    {
        await ListItems();
    }
    public async Task ListItems()
    {
        List<long> ids = new List<long>()
            {
                212035949,
                328191020,
                129895809,
                142719850,
                358562907,
                268154404,
                140107450,
                311011279,
                104502557,
                140804749,
                420049405,
            };
        List<Player> players = new List<Player>(ids.Count);
        foreach (var id in ids)
        {
            var playerInfo = await OpenDota.Player.GetPlayerByIdAsync(id);
            players.Add(playerInfo);
            Debug.Log($"已获取{playerInfo.Profile.Personaname}");
        }
        players = players.OrderByDescending(p => p.MmrEstimate.Estimate.HasValue).ThenByDescending(p => p.MmrEstimate.Estimate.Value).ToList();
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(0));
        }
        foreach (var player in players)
        {
            GameObject obj = Instantiate(item.gameObject, parent);
            PlayerInfoItem itemView = obj.GetComponent<PlayerInfoItem>();
            string name = player.Profile.Personaname;
            string mmr = "未出分";
            if (player.MmrEstimate.Estimate.HasValue)
            {
                mmr = player.MmrEstimate.Estimate.ToString();
            }
            itemView.gameObject.SetActive(true);
            itemView.SetView(name, mmr);
        }
    }
}
