using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface IInteractable
{
    public string InteractionPrompt { get; }

    public bool Interact(Interactor interactor);


}

