﻿using System;
using System.Linq;
using ExampleShaderEditorApp.Model;
using ExampleShaderEditorApp.ViewModels.Nodes;
using NodeNetwork;
using NodeNetwork.Toolkit.ValueNode;
using NodeNetwork.ViewModels;
using NodeNetwork.Views;
using ReactiveUI;

namespace ExampleShaderEditorApp.ViewModels
{
    public class ShaderNodeInputViewModel : ValueNodeInputViewModel<ShaderFunc>
    {
        static ShaderNodeInputViewModel()
        {
            Splat.Locator.CurrentMutable.Register(() => new NodeInputView(), typeof(IViewFor<ShaderNodeInputViewModel>));
        }

        public ShaderNodeInputViewModel(params Type[] acceptedTypes)
        {
            Editor = null;
            ConnectionValidator = new ShaderConnectionValidator
            {
                AcceptedTypes = acceptedTypes
            };
        }
    }

    public class ShaderConnectionValidator : ConnectionValidator
    {
        public Type[] AcceptedTypes { get; set; }

        public override ConnectionValidationResult Validate(PendingConnectionViewModel con)
        {
            Type type = ((ShaderNodeOutputViewModel)con.Output).ReturnType;
            bool isValidType = AcceptedTypes.Contains(type);
            return new ConnectionValidationResult(isValidType,
                isValidType ? null : new ErrorMessageViewModel($"Incorrect type, got {type.Name} but need one of {string.Join(", ", AcceptedTypes.Select(t => t.Name))}"));
        }
    }
}
