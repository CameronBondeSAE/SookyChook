using UnityEngine;
using Unity.Netcode;

public class LightAutomatic : NetworkBehaviour
{
	// Start is called before the first frame update
	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
		// Use the Singleton to get access to it. Use the "Instance" variable
		// (instead of grab dropping actual instances onto variable, or using FindOfObjectOfType)
		
		// Subscribe on the server only
		if (IsServer)
		{
			DayNightManager.Instance.PhaseChangeEvent += DayNightManagerOnPhaseChangeEvent;
		}
		
		// All clients request initial state when they spawn
		if (IsClient)
		{
			RequestInitialStateServerRpc();
		}
	}
	
	public override void OnNetworkDespawn()
	{
		base.OnNetworkDespawn();
		
		if (IsServer && DayNightManager.Instance != null)
		{
			DayNightManager.Instance.PhaseChangeEvent -= DayNightManagerOnPhaseChangeEvent;
		}
	}

	[ServerRpc(RequireOwnership = false)]
	private void RequestInitialStateServerRpc()
	{
		// Server sends current phase to the client that requested it
		ChangeStateClientRpc(DayNightManager.Instance.currentPhase);
	}

	private void DayNightManagerOnPhaseChangeEvent(DayNightManager.DayPhase phase)
	{
		// Server calls RPC to update all clients
		ChangeStateClientRpc(phase);
	}

	[ClientRpc]
	private void ChangeStateClientRpc(DayNightManager.DayPhase phase)
	{
		ChangeState(phase);
	}
	
	// Night lights are now synced
	private void ChangeState(DayNightManager.DayPhase phase)
	{
		if (phase == DayNightManager.DayPhase.Dawn || 
		    phase == DayNightManager.DayPhase.Evening || 
		    phase == DayNightManager.DayPhase.Night ||
		    phase == DayNightManager.DayPhase.Midnight)
		{
			GetComponent<Light>().enabled = true;
		}
		else if (phase == DayNightManager.DayPhase.Morning ||
		         phase == DayNightManager.DayPhase.Noon)
		{
			GetComponent<Light>().enabled = false;
		}
	}
}